using System.ComponentModel.DataAnnotations;

namespace AppMonitoringService.API.Models
{
    /// <summary>
    /// Идентификационные данные устройства
    /// </summary>
    public interface IDeviceData
    {
        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        [Required]
        public string Id { get; set; } 

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required, StringLength(250)]
        public string Name { get; set; } 
    }
}
