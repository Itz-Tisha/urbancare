# 🌆 UrbanCare

**UrbanCare** is a community service web application built using **ASP.NET Core 3.0**.  
It provides a platform for citizens to report city-related issues (like road damage, electricity faults, etc.), and for departments to resolve those issues efficiently.

---

## 🚀 Features

### 👤 Citizen Module
- Register and log in as a **Citizen**
- Report issues in various categories (road, water, electricity, etc.)
- View the status of submitted issues
- Receive a message/notification when the department resolves the issue
- Manage personal profile details

### 🏢 Department Module
- Register and log in as a **Department**
- View all issues reported by citizens
- Update issue status (Pending, In Progress, Resolved)
- Send resolution messages/feedback to the citizen
- Manage department profile information

---

## 🧩 Technologies Used

- **ASP.NET Core 3.0 (MVC)**
- **Entity Framework Core**

---

## ⚙️ Installation & Setup Steps

Follow these steps to run the project locally:



🧭 Step 1 — Clone the Repository
```bash
https://github.com/Itz-Tisha/urbancare

 


### 🥈 Step 2 — Open in Visual Studio or VS Code
```bash
Open the folder in Visual Studio 2019+ or VS Code.

Ensure that you have .NET Core 3.0 SDK installed.
Verify by running:

dotnet --version


The version should be 3.0 or higher.


🥉 Step 3 — Configure the Database Connection

Open the file appsettings.json.

Update the connection string according to your local SQL Server configuration:

"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=UrbanCareDB;Trusted_Connection=True;"
}


🏗️ Step 4 — Apply Entity Framework Migrations

Run these commands in the terminal:

dotnet ef database update


This will create the required tables and seed initial data (if defined).


🧭 Step 5 — Run the Application

You can run the app using either of the following:

dotnet run


or simply press F5 in Visual Studio.



