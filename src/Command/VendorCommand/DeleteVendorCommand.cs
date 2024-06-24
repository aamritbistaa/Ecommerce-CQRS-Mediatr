using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.VendorCommand
{
    public class DeleteVendorCommand : IRequest<Vendor>
    {
        public Guid VendorId { get; set; }
        public Guid UserId { get; set; }
    }
}
