# NewBlogging
A website where users can navigate between articles, and write their own articles.

*Before running the project, please, update-database in packet manager console,
 in order to run seeding method for the essential data, which are roles and categories, to be restored in database.

*You have two options
1- run the project with no experimental data, and begin to use it from zero
2- (recommended) restore the database using the bak file “Yakeen-Blog.bak”  on https://drive.google.com/open?id=11hGtvxS6So7-WnKEdnkm5-JxONbm0ajB,
in order to get experimental/primary data

*After running the project
1- Unauthenticated user:
	*In home, can find all articles
	*can navigate between articles, and choose an article to read it and read the comments
	*choose a category to display articles belonging to it
	*use search: search works with article headers, article texts and author names.
2- authenticated normal user:
	*can comment on any article he wants
3- authenticated admin user:
	*can write a new article, by pressing "write an article" button in navbar
	*can see all his own articles by pressing "your articles" button in navbar

