using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.VendorCommand
{
    public class CreateVendorCommand : IRequest<Vendor>
    {
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string PanNo { get; set; }
        public Guid UserId { get; set; }
    }
}
