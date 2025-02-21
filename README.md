# Introduction

Socket exhaustion = due to too many connection  
Too many connection == too many HttpMessageHandler  
Too many HttpClient == too many HttpMessageHandler  
Single HttpClient reused == too many HttpMessageHandler == too many connection  

So,  

use IHttpClientFactory  
where   
Single Factory + create new HttpClient but reuse HttpMessageHandler  

:)  


### check for status

netstat -ano | findstr TIME_WAIT
