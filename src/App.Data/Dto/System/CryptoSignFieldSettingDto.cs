using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Data.Dto.System;
using Core.Security;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(CryptoSignFieldSetting))]
    public class CryptoSignFieldSettingDto: BaseCryptoSignFieldSettingDto
    {
    }
}
