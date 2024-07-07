using API_ERP.Models.DB.ViewModels;
using API_ERP.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using API_ERP.Interfaces;
using API_ERP.Data;
using Microsoft.Extensions.Options;
using System.Configuration;
using API_ERP.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace API_ERP.Services.Common
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext db;
        private readonly AppSettings _appSettings;
        private IConfiguration Configuration;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDBContext _db,
            IConfiguration _configuration)
        {
            _appSettings = (appSettings?.Value);
            db = _db;
            Configuration = _configuration;
        }

        public async Task<AuthoriseViewModel> Authenticate_Async(WebAPILoginKey data)
        {
            string accessToken = "";
            int apiCode = 0;
            AuthoriseViewModel authoriseViewModel = null;
            int hourForExpire = 48;

            if (data != null)
            {
                string userName = data.Username;
                string userPassword = data.Password;
                string apiAppName = data.ApiAppName;
                string apiKey = data.ApiKey;

                try
                {
                    //string connString = this.Configuration.GetConnectionString("DefaultConnection");
                    using (var trans =
                               new TransactionScope(TransactionScopeOption.Required,
                               new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted },
                               TransactionScopeAsyncFlowOption.Enabled))
                    {
                        apiCode = await (from table in db.WebAPILoginKey
                                         where table.ApiAppName == apiAppName
                                         && table.ApiKey == apiKey
                                         && table.KeyStatus == true
                                         select table.APICode)
                                         .FirstOrDefaultAsync()
                                         .ConfigureAwait(false);
                        trans.Complete();
                    }


                    if (apiCode > 0)
                    {
                        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                        byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                        SecurityTokenDescriptor tokenDescriptor = null;
                        SecurityToken token = null;

                        tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name,apiCode.ToString())
                            }),

                            Expires = DateTime.UtcNow.AddHours(hourForExpire),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                        accessToken = tokenHandler.WriteToken(token);


                        authoriseViewModel = new();
                        authoriseViewModel.access_token = accessToken;
                        authoriseViewModel.token_type = "bearer";
                        authoriseViewModel.expires_sec = hourForExpire * 60 * 60; //hours * 60 min * 60 sec
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);

                    #region error management
                    string projectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                    string pageName = this.GetType().Name;
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    string logSource = System.Reflection.Assembly.GetEntryAssembly().GetName().Name + "_" + pageName + "_" + methodName;

                    string parameterList = JsonConvert.SerializeObject(data);

                    string returnMessage = "";
                    #endregion
                }
            }
            return authoriseViewModel;
        }
    }
}
