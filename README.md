# Blog CQRS

* Michał Krzus
* Dariusz Steblik

# Docker running

* docker run --name blog_mysql -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=blog -p 3306:3306 -d mysql

# Migrations (In Blog.Context)

* dotnet ef migrations add Init
* dotnet ef database update
