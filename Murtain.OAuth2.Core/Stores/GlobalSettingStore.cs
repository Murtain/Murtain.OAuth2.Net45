using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Repositories;
using Murtain.GlobalSettings.Models;
using Murtain.GlobalSettings.Store;
using Murtain.Dependency;
using Murtain.Domain.Services;
using Murtain.Domain.UnitOfWork;
using Murtain.OAuth2.Domain.Repositories;
using Murtain.AutoMapper;

namespace Murtain.OAuth2.Core.Stores
{
    public class GlobalSettingStore : IGlobalSettingStore, IUnitOfWorkService
    {
        private readonly IGlobalSettingRepository globalSettingRepository;

        public GlobalSettingStore(IGlobalSettingRepository globalSettingRepository)
        {
            this.globalSettingRepository = globalSettingRepository;
        }

        public async Task<GlobalSetting> GetSettingAsync(string name)
        {
            return (await globalSettingRepository.FirstOrDefaultAsync(x => x.Name == name))?.MapTo<GlobalSetting>();
        }
        public async Task AddOrUpdateSettingAsync(GlobalSetting setting)
        {
            var model = await globalSettingRepository.FirstOrDefaultAsync(x => x.Name == setting.Name);
            if (model == null)
            {
                await globalSettingRepository.AddAsync(setting.MapTo<Domain.Entities.GlobalSetting>());
            }

            model.Name = setting.Name;
            model.Group = setting.Group;
            model.Scope = setting.Scope;
            model.Description = setting.Description;
            model.Value = setting.Value;
            model.DisplayName = setting.DisplayName;

            await globalSettingRepository.UpdateAsync(model);
        }
        public async Task DeleteSettingAsync(string name)
        {
            var entity = await globalSettingRepository.FirstOrDefaultAsync(x => x.Name == name);
            await globalSettingRepository.RemoveAsync(entity);
        }
        public async Task<List<GlobalSetting>> GetAllSettingsAsync()
        {
            return (await globalSettingRepository.GetAsync(x => true))?.MapTo<List<GlobalSetting>>();
        }
    }
}
