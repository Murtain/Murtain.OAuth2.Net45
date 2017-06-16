using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Murtain.Domain.Entities;
using Murtain.GlobalSettings.Models;
using Murtain.AutoMapper;

namespace Murtain.OAuth2.Domain.Entities
{
    [AutoMap(typeof(GlobalSettings.Models.GlobalSetting))]
    [Table("UX_GLOBAL_SETTING")]
    public class GlobalSetting : AuditedEntityBase
    {
        public GlobalSetting()
        {
            this.Scope = GlobalSettingScope.Application;
        }
        [Column("ID")]
        public override long Id { get; set; }
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        [Required]
        [MaxLength(256)]
        [Column("NAME")]
        public virtual string Name { get; set; }
        /// <summary>
        /// Display Name
        /// </summary>
        [Required]
        [MaxLength(256)]
        [Column("DISPLAY_NAME")]
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// Value of the setting.
        /// </summary>
        [Column("VALUE")]
        public virtual string Value { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [MaxLength(2000)]
        [Column("DESCRIPTION")]
        public virtual string Description { get; set; }
        /// <summary>
        /// Scopes of this setting.
        /// Default value: <see cref="GlobalSettingScope.Application"/>.
        /// </summary>
        [Column("SCOPE")]
        public virtual GlobalSettingScope Scope { get; set; }
        /// <summary>
        /// GlobalSetting Group
        /// </summary>
        [MaxLength(250)]
        [Column("GROUP")]
        public virtual string Group { get; set; }
    }
}
