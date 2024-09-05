using System;

namespace Application.DTO;

public class VerifyUserRequestDto
{
    public string Email { get; set; }
    public string OTP { get; set; }
}
