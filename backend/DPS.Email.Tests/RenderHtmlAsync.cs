namespace DPS.Email.Tests;

public partial class BaseEmailBodyFactoryTest
{
    [TestMethod]
    public async Task RenderResetPasswordEmail()
    {
        var html = await _emailBodyFactory.RenderResetPasswordEmail("myRecoveryUrl");
        Assert.IsTrue(html.Contains("myRecoveryUrl"));
    }
    
    [TestMethod]
    public async Task RenderConfirmAccountEmail()
    {
        var html = await _emailBodyFactory.RenderConfirmAccountEmailAsync("myAccountConfirmUrl");
        Assert.IsTrue(html.Contains("myAccountConfirmUrl"));
    }
}