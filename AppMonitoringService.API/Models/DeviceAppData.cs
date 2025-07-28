using System.ComponentModel.DataAnnotations;

namespace AppMonitoringService.API.Models
{
    /// <summary>
    /// Данные об устройстве, которые приходят от стороннего приложения
    /// </summary>
    public class DeviceAppData
    {
        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Автогенерация ID

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required, StringLength(250)]
        public string Name { get; set; } = string.Empty;

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

        /// <summary>
        /// Версия приложения
        /// </summary>
        [Required]
        public string Version { get; set; } = "1.0.0.0";
    }
}
