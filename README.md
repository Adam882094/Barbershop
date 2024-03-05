<h1>Mock Barbershop Application</h1>
<p>This is a mock barbershop application that lets a customer create an account and book a haircut with a barber. 
  Additionally, it allows an admin to update barbers, haircut options, and haircut prices.</p>
<h3>Usage</h3>
<p>Deployment: http://adambrennan-001-site1.ctempurl.com/</p>
<ol type="A">
  <b><li>Register as a customer</li></b>
  <ol>
    <li>Click on 'Register' on the top right hand of the screen</li>
    <li>Add your Email and password</li>
    <li>You are now logged in and able to use the site!</li>
    <li>You can logout and login with the same credentials</li>
  </ol>
  <b><li>Login as an Admin</li></b>
  <ol>
    <li>Click on 'Login' on the top right of the screen</li>
    <li>Enter the following:<br>
    Email: admin@barbershop.com<br>
    Password: Admin123$
    </li>
    <li>You now have access to the "Barbers" and "Haircut Options" tabs which you may Create, Update, and Delete</li>
  </ol>
</ol>

<h3>Run locally</h3>
<ol>
  <li>Copy the GitHub repository and run in Visual Studio 2022</li>
  <li>Build > Build Solution</li>
  <li>Debug > Start Without Debugging</li>
</ol>
<p>*If times are not updating locally go to Program.cs and update the URL in line 22 to your localhost URL</p>

<h3>Check Database</h3>
<ol>
  <li>Open a copy of SQL Server Management Studio</li>
  <li>Change server name to 'sql9001.site4now.net'</li>
  <li>Set Authentication to 'SQL Server Authentication'</li>
  <li>Login: db_aa5f12_barbershop_admin</li>
  <li>Password: alskdjfhg555@</li>
  <li>Click 'Connect'</li>
</ol>
