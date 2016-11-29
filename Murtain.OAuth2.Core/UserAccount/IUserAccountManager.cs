using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Services;
using Murtain.OAuth2.Core.UserAccount;
using System.Security.Claims;

namespace Murtain.OAuth2.Core
{
    /// <summary>
    /// This interface is about the user account services.
    /// </summary>
    public interface IUserAccountManager : IApplicationService
    {
        /// <summary>
        /// This method get the user account entity by username and password.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        Domain.Entities.UserAccount AuthenticateLocalAsync(string username, string password);
        /// <summary>
        /// This method get the user account entity by username and password.
        /// If the user does not exist, create a user by user cliams. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Domain.Entities.UserAccount AuthenticateExternalAsync(AuthenticateExternalRequest input);
        /// <summary>
        /// This method get the user account entity by subject id.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Domain.Entities.UserAccount GetProfileDataAsync(string subjectId);
        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //GetPagingResponseModel GetPaging(GetPagingRequestModel request);
        ///// <summary>
        ///// 添加
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //AddResponseModel Add(AddRequestModel request);
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //SaveResponseModel Save(SaveRequestModel request);
        ///// <summary>
        ///// 使用手机号码注册
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //RegisterWithTelphoneResponseModel RegisterWithTelphone(RegisterWithTelphoneRequestModel request);
        ///// <summary>
        ///// 获取登录用户信息
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //GetProfileDataResponseModel GetUserProfileData(GetProfileDataRequestModel request);
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
