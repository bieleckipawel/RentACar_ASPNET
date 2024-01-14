# RentACar

Project for ASP.NET Programming


# Installation

Unzip the release zip to the selected folder

## Requirements

 - Asp.Net Core 7 SDK
 - Windows 10 or newer, Linux distro with .NET 7 support, macOS 10.15 or newer

## Database configuration

Application is using SQLite database. Specify its name in appsettings.json file, in SQLiteConnection parameter

## Admin user
On creation of database, the admin user specified in appsettings.json is created. Specify its data in AdminUser value.

## Sample data
There is sample car list (10 cars) created on database creation.

# Usage (anonymous)

When user is not logged in, the index file with invitation to sign up is shown. User can log in or sign in.

![anon user view](https://i.ibb.co/hHqmpCB/anon.png)

You can register or login using navigation bar or link in the invitation
After registering user would need to visit the office to get their data confirmed due to legal reasons.

# Usage (logged in user)

When user is logged in, user will see greeting and logout option in navigation bar.

![enter image description here](https://i.ibb.co/Fbvtm9b/logged.png)

User also will see Rentals and Cars button. Rentals button direct to a list where user can see his rentals, cars button direct to car list, where user can see car list and rent button.

Car list:

![enter image description here](https://i.ibb.co/S3S7n6F/carlist.png)

User can click "Rent" button here and rent selected car
Add rental option:

![enter image description here](https://i.ibb.co/m64nhVs/addrental.png)

User can specify car, rental date and return date. Rental date cannot be later than Return date. Car must be available to rent.
After creating rental, user will be redirected to rental list where rent details are available:

![enter image description here](https://i.ibb.co/cgKJs9q/rentalsuccess.png)

The rent is successful message is depending on account verification. Employee would need to confirm user data upon first rental.
# Usage (admin)

![enter image description here](https://i.ibb.co/8MYV8z6/admin.png)

## Rental menu
In Rentals menu, admin can rent a car to some customer (the customer select list is active for admins) or delete a rental:

![enter image description here](https://i.ibb.co/28ZDJ0T/admin-rental.png)

![enter image description here](https://i.ibb.co/Px6c51g/admin-rentaladd.png)

Delete rental would need a additional confirmation:

![enter image description here](https://i.ibb.co/WkJBCLF/admin-deleterental.png)

##  Car menu
Admins can create new car, edit a car, delete a car or rent a car

![enter image description here](https://i.ibb.co/w0Gsq9m/admincarlist.png)

## User menu
Admins have access to user list, where they could create new user manually, verify user, delete users and edit users.

![enter image description here](https://i.ibb.co/SvtCdzX/admin-user.png)

While editing user, admin can tick verify button and choose roles for the user:

![enter image description here](https://i.ibb.co/D9DcDbW/admin-edituser.png)
