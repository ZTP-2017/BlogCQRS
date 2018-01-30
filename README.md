# Blog CQRS

* Michał Krzus
* Dariusz Steblik

# Docker running

* docker run --name blog_write -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=blog_write -p 3306:3306 -d mysql

* docker run --name blog_read -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=blog_read -p 3307:3306 -d mysql

# Migrations (Blog.ContextRead & Blog.ContextWrite)

* dotnet ef migrations add Init
* dotnet ef database update
