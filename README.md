# questionsApi
the api runs on localhost

before starting define in the appsettings.development.json the correct connection string to your database

also define the missing fields in the email smtp

after that just press f5 and it should work



Additional notes:

the offset is ignored when limit is null, since it wouldn't make sense

in the put call an id is required but since it already comes in the object itself, that id is ignored throughout the call

added swagger support for development aid
