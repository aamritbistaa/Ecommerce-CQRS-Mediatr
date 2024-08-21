
## Description

Role - [Customer, Vendor, Admin, SuperAdmin]

Size - [XS, S, M, L, XL, XXL]
Color will be a seeded, drop down, with also ability to add custom data
Country and City are seeded and predefined

OrderItem.Status - [Approved, Cancelled, Placed]

OrderStatus - [Shippied, Delivered, Ready for shipping]

**Customer**

- View product
- Add To wishlist/cart
- Select item from wish list and place order
- When item has been purchased, Customer should be able to add rating and review to the product
- Customer can post question and review that will be in Product microservice
- Before receiving the product, Customer should enter the respective otp


**Vendor**
- Post product, Update, add discount or delete the product
- When item is ordered the status will be placed, respective vendor will be send with the conformation about the availibility of the product, which when conformed will be changed to approved
- When all the product with the status approved, it will automatically be moved for next stage [Ready for shipping] 



**Admin/ SellerOfficer**

- When order are conformed, Order Status will be changed to Shipping
- When shipped, admin will take otp from customer and then the status will be changed to delivered



**Data flow**

User views the product

adds items to wish list list

proceeds to checkout

items are added to orderitem 

vendor will be notified about the orderitem

vendor approves it or denes it, order will be updated accordingly

User approves it or system automatically approves it and new otp is sent to the user

admin will have a portal to complete the order based on order it and otp



User will be able to view past orders as well.






After order is placed, a otp is sent to customer, which when entered the product should be set as verified





it will be approved by collection place or admin, then otp will be sent, 







## Todo

User can register as customer
admin can change any user to vendor or customer,
when admin changes user to vendor,
vendor can register his shop
vendor can add product
In user-credetial model, make email and username unique
Customer or anyone can look at the product,
customer should be able to add/remove item to cart
database model validation
Logger using serilog
File upload


hash the password before saving it
Test cases
