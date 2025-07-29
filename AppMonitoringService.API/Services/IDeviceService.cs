using AppMonitoringService.API.Models;

namespace AppMonitoringService.API.Services
{
    /// <summary>
    /// Интерфейс сервиса устройства 
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Добавление данных об устройстве (опционально, для тестирования работы API)
        /// </summary>
        /// <param name="data"></param>
        void AddData(DeviceAppData data);

        /// <summary>
        /// Получение всех данных
        /// </summary>
        /// <returns></returns>
        List<DeviceAppData> GetAllDevices();

        /// <summary>
        /// Получение данных о конкретном устройстве
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<DeviceAppData> GetDeviceSessions(string id);

        /// <summary>
        /// Удаление старых записей о сессиях активности конкретного устройства
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        int DeleteOldRecords(string id, DateTime date);

        /// <summary>
        /// Бэкап данных в файл
        /// </summary>
        void Backup();
    }
}
