namespace CropDiseaseDetection.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public string Crop { get; set; }
        public string DiseaseName { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
    }
}