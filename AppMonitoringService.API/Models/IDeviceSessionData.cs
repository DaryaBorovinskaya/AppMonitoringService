using System.ComponentModel.DataAnnotations;

namespace AppMonitoringService.API.Models
{
    /// <summary>
    /// Сессии активности устройства
    /// </summary>
    public interface IDeviceSessionData
    {
        /// <summary>
        /// Время включения
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время выключения
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }
    }
}
