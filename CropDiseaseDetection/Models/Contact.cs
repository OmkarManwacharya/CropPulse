using System.ComponentModel.DataAnnotations.Schema;

namespace CropPulse.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // If you want to save this, you MUST run the migration steps above
        public string Subject { get; set; }

        public string Message { get; set; }
    }
}