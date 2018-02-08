# Blog CQRS

* Michał Krzus
* Dariusz Steblik

# Docker

* docker run -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=blog_write -p 3306:3306 -d mysql
* docker run -p 27017:27017 -d mongo  

# Migrations (Blog.WriteSide)

* dotnet ef migrations add Init
* dotnet ef database update
