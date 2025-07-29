using AppMonitoringService.API.Models;
using AppMonitoringService.API.Services;
using System.Collections.Generic;
using System.Text.Json;

namespace AppMonitoringService.API.Backup
{
    /// <summary>
    /// Бэкап данных, хранящихся in-memory
    /// </summary>
    public class DeviceAppDataBackup
    {
        /// <summary>
        /// Сервис логирования
        /// </summary>
        private readonly ILogger<DeviceService> _logger;

        public DeviceAppDataBackup(ILogger<DeviceService> logger)
        {
            _logger = logger;

        }

        /// <summary>
        /// Сохранить данные в файл 
        /// </summary>
        /// <param name="devices"></param>
        public void SaveBackup(List<DeviceAppData> devices)
        {
            try
            {
                JsonSerializerOptions? options = new JsonSerializerOptions { WriteIndented = true };
                string? json = JsonSerializer.Serialize(devices, options);

                string backupDirectory = "/app/devices_backup";
                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                TimeZoneInfo utcPlus7 = TimeZoneInfo.FindSystemTimeZoneById("Asia/Novosibirsk"); 
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, utcPlus7);

                string fileName = $"devices_backup_{localTime.ToString("ddMMyyyy_HHmmss")}.json";
                string path = Path.Combine(backupDirectory, fileName);

                File.WriteAllText(path, json);
                _logger.LogInformation("Бэкап данных успешно сохранен");
                _logger.LogInformation("Бэкап сохраняется в: ./AppMonitoringService.API/devices_backup/");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении бэкапа");
            }
        }
    }
}
