using System.ComponentModel.DataAnnotations;

namespace AppMonitoringService.API.Models
{
    /// <summary>
    /// Данные об устройстве, которые приходят от стороннего приложения
    /// </summary>
    public class DeviceAppData : IDeviceData, IDeviceSessionData, IAppData
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Автогенерация ID

        [Required, StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Version { get; set; } = "1.0.0.0";
    }
}
