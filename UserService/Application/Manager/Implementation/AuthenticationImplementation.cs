using System;
using System.Net;
using System.Net.Mail;
using Application.Common;
using Application.DTO;
using Application.Manager.Interface;
using Domain.Enum;
using Domain.Interface;
using Microsoft.Extensions.Configuration;

namespace Application.Manager.Implementation;

public class AuthenticationImplementation : IAuthenticationImplementaton
{
    private IConfiguration _configuration;
    private readonly string _email;
    private readonly string _password;
    private readonly IUserCredentialsService _userCredentialsService;
    public AuthenticationImplementation(IConfiguration configuration, IUserCredentialsService userCredentialsService)
    {
        _configuration = configuration;

        _email = _configuration["EmailCredentials:username"];
        _password = _configuration["EmailCredentials:password"];
        _userCredentialsService = userCredentialsService;
    }
    public async Task<ServiceResult<string>> RequestToVerifyUser(string email)
    {
        var user = await _userCredentialsService.GetByEmail(email);
        if (user == null)
        {
            //? User Doesnot exist in the system
            return new ServiceResult<string>
            {
                StatusCode = StatusCode.BadRequest,
                Message = Messages.BadRequest,
                Data = "User with email doesnot exist"
            };
        }

        if (user.UserStatus == UserStatus.Unverified)
        {
            var otp = await SendOTP(email);
            user.OTP = otp.ToString();
            var result = await _userCredentialsService.UpdateItemAsync(user);
            if (result == false)
            {
                return new ServiceResult<string>
                {
                    StatusCode = StatusCode.ServerError,
                    Message = Messages.ServerError,
                    Data = "Error!! Error saving OTP in database"
                };
            }
            return new ServiceResult<string>
            {
                StatusCode = StatusCode.Success,
                Message = Messages.Success,
                Data = "OTP has been send."
            };
        }
        return new ServiceResult<string>
        {
            StatusCode = StatusCode.BadRequest,
            Message = Messages.BadRequest,
            Data = "Invalid!! User status is not unverified"
        };
    }
    private async Task<int> SendOTP(string mailingAddress)
    {
        var generateNumber = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 1000000);

        var message = @$"
        Dear Sir/Madam,
        ATTN : Please do not reply to this email.This mailbox is not monitored and you will not receive a response.
        Your One Time Password (OTP ) is {generateNumber.ToString()}.
        If you have any queries, Please contact us at,
        BuyNinja,
        Kathmandu, Nepal.
        Phone # 977-01-4255306
        Email Id: support@nchl.com.np
        Warm Regards,
        Nepal Clearing House Limited.
        ";

        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_email, _password);
            client.UseDefaultCredentials = false;

            var mailMessage = new MailMessage(_email, mailingAddress, "User Conformation, OTP.", message);
            await client.SendMailAsync(mailMessage);
        }
        return generateNumber;
    }

    public async Task<ServiceResult<string>> VerifyUser(VerifyUserRequestDto request)
    {
        var user = await _userCredentialsService.GetByEmail(request.Email);
        if (user == null)
        {
            //? User Doesnot exist in the system
            return new ServiceResult<string>
            {
                StatusCode = StatusCode.BadRequest,
                Message = Messages.BadRequest,
                Data = "User with email doesnot exist"
            };
        }
        if (user.UserStatus == UserStatus.Unverified && user.OTP == null)
        {
            await RequestToVerifyUser(request.Email);
            return new ServiceResult<string>
            {
                StatusCode = StatusCode.BadRequest,
                Message = Messages.BadRequest,
                Data = "Please try again."
            };
        }
        if (user.OTP == request.OTP)
        {
            user.UserStatus = UserStatus.Verified;
            await _userCredentialsService.UpdateItemAsync(user);
            return new ServiceResult<string>
            {
                StatusCode = StatusCode.Success,
                Message = Messages.Success,
                Data = "User has been verified."
            };
        }
        return new ServiceResult<string>
        {
            StatusCode = StatusCode.BadRequest,
            Message = Messages.BadRequest,
            Data = "Invalid!! request input error"
        };
    }
}
