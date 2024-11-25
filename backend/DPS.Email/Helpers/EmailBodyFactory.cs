using DPS.Email.Templates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DPS.Email.Helpers;

public class EmailBodyFactory(HtmlRenderer htmlRenderer)
{
    public async Task<string> RenderResetPasswordEmail(string recoveryUrl)
    {
        return await RenderHtmlAsync(typeof(ResetPassword), new Dictionary<string, object?>()
        {
            { "ResetPasswordUrl", recoveryUrl }
        });
    }

    public async Task<string> RenderConfirmAccountEmailAsync(string accountConfirmUrl)
    {
        return await RenderHtmlAsync(typeof(ConfirmAccount), new Dictionary<string, object?>()
        {
            { "AccountConfirmUrl", accountConfirmUrl }
        });
    }

    private async Task<string> RenderHtmlAsync(Type template, IDictionary<string, object?>? dictionary = null)
    {
        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            dictionary ??= new Dictionary<string, object?>();
            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await htmlRenderer.RenderComponentAsync(template, parameters);

            return output.ToHtmlString();
        });

        return html;
    }
}