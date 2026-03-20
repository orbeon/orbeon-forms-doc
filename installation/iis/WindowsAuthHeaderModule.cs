using System;
using System.Web;

public class WindowsAuthHeaderModule : IHttpModule
{
    public void Init(HttpApplication context)
    {
        context.AuthorizeRequest += OnAuthorizeRequest;
    }

    private void OnAuthorizeRequest(object sender, EventArgs e)
    {
        var app = (HttpApplication)sender;
        var user = app.Context.User;
		var identity = user != null ? user.Identity : null;

        if (identity != null && identity.IsAuthenticated)
        {
            // Strip DOMAIN\ prefix, keep just the username
            var username = identity.Name;
            if (username.Contains("\\"))
                username = username.Split('\\')[1];

            // HTTP_ prefix + uppercase = request header forwarded by ARR
            // HTTP_ORBEON_USERNAME  →  Orbeon-Username header
            app.Context.Request.ServerVariables.Set("HTTP_ORBEON_USERNAME", username);
        }
    }

    public void Dispose() { }
}