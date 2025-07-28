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
        /// Путь к файлу
        /// </summary>
        private readonly string _backupFilePath;

        /// <summary>
        /// Сервис логирования
        /// </summary>
        private readonly ILogger<DeviceService> _logger;

        public DeviceAppDataBackup(ILogger<DeviceService> logger)
        {
            _logger = logger;
            _backupFilePath = "devices_backup.json";

        }

        /// <summary>
        /// Подгрузить данные из файла (если есть)
        /// </summary>
        /// <returns></returns>
        public List<DeviceAppData> LoadData()
        {
            List<DeviceAppData> devices;

            try
            {
                if (File.Exists(_backupFilePath))
                {
                    string json = File.ReadAllText(_backupFilePath);
                    devices = JsonSerializer.Deserialize<List<DeviceAppData>>(json) ?? new ();
                    _logger.LogInformation("Данные успешно загружены из бэкапа");
                }
                else
                {
                    devices = new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при загрузке данных из бэкапа");
                devices = new ();
            }
            return devices;
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
                File.WriteAllText(_backupFilePath, json);
                _logger.LogInformation("Бэкап данных успешно сохранен");
                _logger.LogInformation("Бэкап сохраняется в: {Path}", Path.GetFullPath(_backupFilePath));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении бэкапа");
            }
        }
    }
}
