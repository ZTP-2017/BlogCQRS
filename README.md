# Blog CQRS

* Michał Krzus
* Dariusz Steblik

# Docker running

* docker run --name blog_cqrs -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=blog_cqrs -p 3306:3306 -d mysql

# Migrations (In Blog.Context)

* dotnet ef migrations add Init
* dotnet ef database update
