using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorWebAssemblyOpenIdTest.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddScoped(sp => new HttpClient
			{ BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			builder.Services.AddOidcAuthentication(options =>
			{
				// https://dev-69746596.okta.com/.well-known/openid-configuration
				options.ProviderOptions.Authority = "https://dev-69746596.okta.com/";
				options.ProviderOptions.ClientId = "0oa7ocehkhG2vowuy5d7";
				options.ProviderOptions.ResponseType = "code";
				options.ProviderOptions.PostLogoutRedirectUri = "http://localhost:4200/";
				options.ProviderOptions.RedirectUri = "http://localhost:4200/cb";
				options.ProviderOptions.DefaultScopes.Add("openid");
				options.ProviderOptions.DefaultScopes.Add("profile");
			});

			await builder.Build().RunAsync();
		}
	}
}