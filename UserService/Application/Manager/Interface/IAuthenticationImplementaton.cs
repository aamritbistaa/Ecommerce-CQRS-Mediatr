using System;
using Application.Common;
using Application.DTO;

namespace Application.Manager.Interface;

public interface IAuthenticationImplementaton
{
    Task<ServiceResult<string>> RequestToVerifyUser(string mailingAddress);
    Task<ServiceResult<string>> VerifyUser(VerifyUserRequestDto request);
}
