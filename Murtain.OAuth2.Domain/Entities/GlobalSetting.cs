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
    public class GlobalSetting : AuditedEntityBase
    {
        public GlobalSetting()
        {
            this.Scope = GlobalSettingScope.Application;
        }
        public override long Id { get; set; }
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string Name { get; set; }
        /// <summary>
        /// Display Name
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// Value of the setting.
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [MaxLength(2000)]
        public virtual string Description { get; set; }
        /// <summary>
        /// Scopes of this setting.
        /// Default value: <see cref="GlobalSettingScope.Application"/>.
        /// </summary>
        public virtual GlobalSettingScope Scope { get; set; }
        /// <summary>
        /// GlobalSetting Group
        /// </summary>
        [MaxLength(250)]
        public virtual string Group { get; set; }
    }
}
