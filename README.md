##Development decisions

* Assumes 10 devs
* assumes work weekdays only
* assumes assignment cannot happen backwards
* assumes auth not required as internal app but can be added if required
* SOA approach - Imagined Employees were kept in seperate repository and created a service consumed internally to expose them
* Imagined that other apps may want to create BAU assignments
* front end kept to be a nice lightweight SPA, consumer of services, can be changed with ease
* BAU assignment logic is broken out into own class which is injected, can be updated easily (could have potentially been its own service) 
* Requirejs used for front end to make it simpler and more understandable, also library is used for wheel of fortune, only needs to be lightweight currently, so didnt need to bring in any big frameworks
* API lacks standard design (perhaps RPC-like), though simple at the moment, may require rethought if it became more complicated
* database is in dal layer just because 

##improvements/ todo

* add in holdays and consider sickdays 
* caching for employees (not likely to change every day)
* no tests for front end
* break contracts out into own proj (believe this was mentioned)
* view.js needs sorting out (vals being broke out into consts/ general refactoring) 
* frontend is a bit bleak
* rename HomeController
* more tests in general, in particular validation around assign employee
* minify winwheel
* reduce html further by creating wheel of fortune component


##to run

* download
* open in visual studio 
* repoint bau services' connection string to mdf file location (needs full file path??)
* run
