<h1>♦️ OnlineStore_MVC ♦️</h1>

<h3>
  <b>~~~</b> <ins>Remake</ins> of the old OnlineStore that was made for KPI project. (But better) <b>~~~</b>
</h3>

This is the example of my skills🎯 in <i>ASP.NET Core 3.1</i>. In this project, I was using 3-layer architecture. 

➡️ <b>DAL</b> represented as layer with base repository methods to work with database entity-table's.<br>
     Also contains a Unit of Work pattern that collect all the repositories into one unit to make easier access for the repositories.<br>
		To work with database I was using <i>Entity Framework Core</i>.<br>
		Main methods are covered by <i>NUnit tests</i> with <i>Moq</i>.
		
➡️ <b>BLL</b> contains all the main logic to work with online store like:<br> 
    working with the shopping cart, showing product list, searching by categories/namings/prices.<br>
		To transfer data I was using <i>AutoMapper</i> to Map base objects created in folder "DTO"
    
➡️ <b>PL</b> is MVC-based layer containing controllers to work with BLL methods.<br>
    It contains views made mostly with bootstrap and partly with own-written CSS.<br>
		For authentication I was using <i>ASP.NET Core Identity</i> with <i>JWT</i>
		
✖️To create email-messages for authentication I've added EmailService using <i>NETCore.MailKit</i>


ℹ️<b>Main pages screenshots:</b>

<b>Index home for administration view:</b>
![image](https://user-images.githubusercontent.com/71894616/184533369-3ba4cb38-3cb3-46ab-9465-b5efe7aedcc0.png)

<b>Index home for customer view:</b>
![image](https://user-images.githubusercontent.com/71894616/184533415-fcfb3f3d-155d-43ee-aca3-7190516f9fe9.png)

<b>Shopping cart modal view:</b>
![image](https://user-images.githubusercontent.com/71894616/184533459-28b8b4e6-66d4-429d-98eb-07aff4c352bd.png)

<b>Wishlist modal view:</b>
![image](https://user-images.githubusercontent.com/71894616/184533476-19da1650-34c6-4428-8900-24022b0c3596.png)

<b>Product description view:</b>
![image](https://user-images.githubusercontent.com/71894616/184533502-a6cd14d7-e620-4e5d-bf86-4c33e8d1b35e.png)

<b>Receipts modal view:</b>
![image](https://user-images.githubusercontent.com/71894616/184533527-e5b6b658-c7ae-4ae4-9cf5-00bf35aebcce.png)
