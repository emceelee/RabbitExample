# RabbitExample

Install Erlang
Install RabbitMQ

https://www.rabbitmq.com/management.html

.erlang.cookie
Copy from C:\Windows\System32\config\systemprofile
To
C:\users\matthew_lee

Command Line (Administrator):  
set HOMEDRIVE=C:\Users\matthew_lee
C:\Program Files\RabbitMQ Server\rabbitmq_server-3.7.7\sbin
rabbitmq-plugins enable rabbitmq_management

Enables console (disabled by default):
http://localhost:15672/
guest/guest

https://www.rabbitmq.com/install-windows-manual.html
rabbitmq-service install
Rabbitmq-service start