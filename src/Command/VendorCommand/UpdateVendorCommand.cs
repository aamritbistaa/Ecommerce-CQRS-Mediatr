using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.VendorCommand
{
    public class UpdateVendorCommand:IRequest<Vendor>
    {
        public Guid VendorId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set;}
        public string PanNo {  get; set;}
    }
}
