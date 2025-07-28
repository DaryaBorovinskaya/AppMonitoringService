using System.ComponentModel.DataAnnotations;

namespace AppMonitoringService.API.Models
{
    /// <summary>
    /// Данные о приложении
    /// </summary>
    public interface IAppData
    {
        /// <summary>
        /// Версия приложения
        /// </summary>
        [Required]
        public string Version { get; set; } 
    }
}
