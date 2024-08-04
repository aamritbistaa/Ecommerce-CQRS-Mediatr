namespace CQRS.Ecommerce.Application;

public class ServiceResult<T>
{
    public StatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public T Data { get; set; }
}

public enum StatusCode
{
    Information = 100,
    Success = 200,
    Redirection = 300,
    ClientError = 400,
    ServerError = 500
}
public class Message
{
    public static string Success = "Displaying Item.";
    public static string Null = "Please add items First.";
    public static string NotFound = "Item with specified id does not exist.";
    public static string ErrorWhileCreating = "Somethingwent wrong while adding item.";
    public static string SuccessWhileCreating = "Item has been added successfully.";
    public static string ErrorWhileUpdating = "Somethingwent wrong while updating item.";
    public static string SuccessWhileUpdaing = "Item has been updated successfully.";
    public static string ErrorWhileDeleting = "Something went wrong while deleting item.";
    public static string SuccessWhileDeleting = "Item deleted successfully.";
}