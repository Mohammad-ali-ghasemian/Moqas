﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moqas.Model.Data;
using Moqas.Model.Settings;
using Moqas.Service.Chat;

namespace Moqas.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSettingsController : ControllerBase
    {
        MoqasContext _context;
        public CustomerSettingsController(MoqasContext context)
        {
            _context = context;
        }

        [HttpPost("insert-setting")]
        public async Task<IActionResult> InsertSetting(GetCustomerSettings settings)
        {
            return await CustomerSettingsService.InsertSetting(this, _context, settings);
        }

        [HttpGet("get-setting")]
        public async Task<IActionResult> GetSetting(int customerId, string type)
        {
            return await CustomerSettingsService.GetSetting(this, _context, customerId, type);
        }

        [HttpPut("update-type")]
        public async Task<IActionResult> UpdateType(int settingId, string newType)
        {
            return await CustomerSettingsService.UpdateType(this, _context, settingId, newType);
        }

        [HttpPut("update-key-value")]
        public async Task<IActionResult> UpdateKeyValue(int settingId, UpdateKeyValue newKeyValue)
        {
            return await CustomerSettingsService.UpdateKeyValue(this, _context, settingId, newKeyValue);
        }

        [HttpDelete("delete-setting")]
        public async Task<IActionResult> DeleteSetting(int settingId)
        {
            return await CustomerSettingsService.DeleteSetting(this, _context, settingId);
        }
    }
}
