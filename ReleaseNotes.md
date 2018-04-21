# Mission Support Web Release Notes 

##What’s New:

###### We’ve updated the sites in this new release.
Sites will no longer have an organization linked to their name. This added some confusion since multiple organizations can be linked to a site. So we will, allow you to have multiple organization sign up to a site. 
Sites now have a pop-up menu when the title of the site is clicked on. The site information on the label will now only contain a brief intro to the location. When you click on the title now, you will be able to see more detailed information about the site to help make decisions.
###### Calendar updated:
We now have a main calendar that lets you know about recent trips planned. Mission Support also has a calendar in each site that lets users add trips to their main calendar.
###### Admins updated:
Can verify users and sites, and/or delete them as they see fit in order to moderate the website. Currently the admin functionality only works on the client side and does not affect the server.

Bug fixes:
The admin page bug was fixed with this release. Before the table was populating twice every time you navigate away from it. Now, it doesn’t do that anymore. 

Known Defects:
Sometimes the map on the sites page incorrectly populates the addresses because of fake addresses. You can temporarily fix that by refreshing the page and it goes back to normal. Or you can delete the address from the database and it will reset to normal. 

Mission Support Mobile Release Notes 

What’s New:
We’ve updated the sites in this new release.
The Add A New Site Form will now allow you to add a description in addition to Name and Address of each site.
Sites now have a pop-up menu when the title of the site is clicked on. The site information on the label will now only contain the name of the site. When you click on the name, you will be able to see more detailed information about the site.
The Application Logo is now updated with the new awesome hand-designed logo by our team!

Bug fixes:
The UI of the mobile applications have been improved to a better quality since the last release
The Add Site button on the Sites page is now more visible.
The images on the About Page have been properly embedded and will now show up accordingly on screen.

Known Defects:
The application is currently running on a local temporary database.  We were looking into connecting the mobile app with the same hosted database for the web version through a REST api or any of the like.  Unfortunately, we couldn’t finish this as the semester ended.  Future implementations will need to take this into considerations.
One unfinished feature includes the Profile Page for each logged in user.
Another unfinished feature includes the Calendar Page and registering the DatePicker data into database.  This will require an external service/package such as Telerik as it’s not a built in library for Xamarin Forms.
One defect we were still resolving involve the side Menu Bar.  Future implementations will need to make this more interactive and update in real-time as well as include links for users to go back and register/login if they so choose.
