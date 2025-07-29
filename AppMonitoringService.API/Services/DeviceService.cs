using AppMonitoringService.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Concurrent;
using AppMonitoringService.API.Backup;


namespace AppMonitoringService.API.Services
{
    public class DeviceService : IDeviceService
    {
        private List<DeviceAppData> _devices;

        /// <summary>
        /// Сервис логирования
        /// </summary>
        private readonly ILogger<DeviceService> _logger;  
        private readonly DeviceAppDataBackup _backup;

        public DeviceService(ILogger<DeviceService> logger)
        {
            _logger = logger;
            _backup = new(_logger);
            _devices = new List<DeviceAppData>();
        }

        public void AddData(DeviceAppData data)
        {
            _logger.LogInformation("{count}", _devices.Count);
            _devices.Add(data);
            _logger.LogInformation("Добавлено устройство: {deviceId}", data.Id);
            
        }

        public List<DeviceAppData> GetAllDevices()
        {
            _logger.LogInformation("Запрошен список всех устройств");
            return _devices;
        }

        public List<DeviceAppData> GetDeviceSessions(string id)
        {
            _logger.LogInformation("Запрошены все записи об устройстве {deviceId}", _devices.First(a => a.Id == id).Id);
            return _devices.Where(a => a.Id == id).ToList();
        }

        public int DeleteOldRecords(string id, DateTime date)
        {
            int countDelete = _devices.RemoveAll(a => (
                a.Id == id &&
                (a.EndTime.Year < date.Year
                || a.EndTime.Month < date.Month
                || a.EndTime.Day <= date.Day)
            ));
            _logger.LogInformation("Удалено {Count} устаревших записей", countDelete);

            return countDelete;
        }

        public void Backup()
        {
            _backup.SaveBackup(_devices);
        }
    }
}
