using System;
using System.Collections.Generic;
using System.Text;
using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Models
{
    public interface IApplicationSettingsContext
    {
        DbSet<ApplicationSetting> ApplicationSetting { get; set; }
    }
}
