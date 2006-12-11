
Atlanta
=======

A project to demonstrate best use of NHibernate and Spring.Net to create an enterprise application.

To build:

1.  Open a command prompt at the root directory;
2.  Type 'env'  (to set the environment to include NAnt in the path);
3.  Type 'nant' (to create a default debug build).

That's it!

Access the application using:

http://localhost/atlanta            (the example application)
http://localhost/atlanta/design     (the design documentation)


Some example NAnt scenarios:

Command                                 Description
=======                                 ===========

nant                                    Perform a debug build (same as: nant debug build)
nant clean                              Clean the debug build (same as: nant debug clean)
nant release build                      Perform a release build
nant release clean                      Clean a release build
nant release build -D:notest=true       Perform a release build, but supress creation (and running) of tests
nant -D:ignore=Application              Ignore the project labelled 'Application' when it is reached

