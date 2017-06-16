using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.AutoMapper;

namespace Murtain.OAuth2.Domain.Entities
{
    [Table("UX_USER_ACCOUNT")]
    public class UserAccount : SoftDeleteEntityBase
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50)]
        [Column("NAME")]
        public virtual string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        [Column("NICK_NAME")]
        public virtual string NickName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [MaxLength(50)]
        [Column("BIRTHDAY")]
        public string Birthday { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Column("AGE")]
        public int Age { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        [Column("MOBILE")]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        [Column("EMAIL")]
        public virtual string Email { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        [MaxLength(250)]
        [Column("ADDRESS")]
        public virtual string Address { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [MaxLength(50)]
        [Column("CITY")]
        public virtual string City { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        [MaxLength(50)]
        [Column("PROVINCE")]
        public virtual string Province { get; set; }
        /// <summary>
        /// 所在国家
        /// </summary>
        [MaxLength(50)]
        [Column("COUNTRY")]
        public virtual string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(50)]
        [Column("GENDER")]
        public virtual string Gender { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(2000)]
        [Column("AVATAR")]
        public virtual string Avatar { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(250)]
        [Column("PASSWORD")]
        public virtual string Password { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column("SUB_ID")]
        public virtual string SubId { get; set; }
        /// <summary>
        /// 登录提供程序
        /// </summary>
        [MaxLength(50)]
        [Column("LOGIN_PROVIDER")]
        public virtual string LoginProvider { get; set; }
        /// <summary>
        /// 登录提供程序Id
        /// </summary>
        [MaxLength(50)]
        [Column("LOGIN_PROVIDER_ID")]
        public virtual string LoginProviderId { get; set; }
    }
}
