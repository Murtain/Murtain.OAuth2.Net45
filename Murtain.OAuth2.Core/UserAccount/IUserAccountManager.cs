using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Services;
using Murtain.OAuth2.Core.UserAccount;
using System.Security.Claims;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.Domain.UnitOfWork;

namespace Murtain.OAuth2.Core
{
    /// <summary>
    /// This interface is about the user account services.
    /// </summary>
    public interface IUserAccountManager : IApplicationService,IUnitOfWorkService
    {
        /// <summary>
        /// This method get the user account entity by username and password.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        Task<Domain.Entities.UserAccount> AuthenticateLocalAsync(string username, string password);
        /// <summary>
        /// This method get the user account entity by username and password.
        /// If the user does not exist, create a user by user cliams. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Domain.Entities.UserAccount> AuthenticateExternalAsync(AuthenticateExternalRequest input);
        /// <summary>
        /// This method get the user account entity by subject id.
        /// </summary>
        /// <param name="subId"></param>
        /// <returns></returns>
        Task<Domain.Entities.UserAccount> GetProfileDataAsync(string subId);
        /// <summary>
        /// 手机号注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task LocalRegistrationAsync(LocalRegistrationRequestModel request);
        ///// <summary>
        ///// 设置/修改密码
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //SetPasswordResponseModel SetPassword(SetPasswordRequestModel request);
        ///// <summary>
        ///// 设置/修改邮箱
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //SetEmailResponseModel SetEmail(SetEmailRequestModel request);
    }
}
