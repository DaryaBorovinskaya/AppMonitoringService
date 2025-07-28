using System.ComponentModel.DataAnnotations;

namespace AppMonitoringService.API.Models
{
    /// <summary>
    /// Данные об устройстве (формат со списком сессий)
    /// </summary>
    public class DeviceData : IDeviceData, IAppData
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Автогенерация ID

        [Required, StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Version { get; set; } = "1.0.0.0";

        /// <summary>
        /// Список сессий активности устройства
        /// </summary>
        [Required]
        public List<DeviceSession> Sessions { get; set; }
    }
}
