namespace DPS.Email.Helpers;

public static class SubjectFactory
{
    public static string GetResetPasswordSubject()
        => "DPS - Reset password request";

    public static string GetConfirmAccountSubject()
        => "DPS - Confirm your account registration";
}