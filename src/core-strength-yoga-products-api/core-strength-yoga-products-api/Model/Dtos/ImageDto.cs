namespace core_strength_yoga_products_api.Model.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Alt { get; set; }
        public string Path { get; set; }

        public static ImageDto Resolve(Image image)
        {
            return new ImageDto
            {
                Id = image.Id,
                ImageName = image.ImageName,
                Alt = image.Alt,
                Path = image.Path
            };
        }

        public static Image Resolve(ImageDto imageDto)
        {
            return new Image
            {
                Id = imageDto.Id,
                ImageName = imageDto.ImageName,
                Alt = imageDto.Alt,
                Path = imageDto.Path
            };
        }
    }
}
