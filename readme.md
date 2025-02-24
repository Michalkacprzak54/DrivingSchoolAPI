# Driving School – Backend API  

## 📌 Project Link  
Backend (this repo)  
[ Frontend](https://github.com/Michalkacprzak54/DrivingSchoolReactApp)
## 📖 Project Description  
The **Driving School** project is a **.NET Web API** application designed to manage driving lessons, students, instructors, and scheduling. The backend handles all business logic, database interactions, and provides a REST API for the frontend to interact with. The system includes functionalities for lesson scheduling, instructor management, student records, and payments. It also implements authentication and role-based access control for students, instructors, and administrators.  

## 🎯 Project Goals  
### ⚙️ Backend  
- Develop a **.NET 8 Web API** backend to manage driving school operations.  
- Implement business logic using **Entity Framework Core**.  
- Provide a RESTful API for frontend communication.  
- Ensure secure authentication and authorization using **JWT Tokens**.  
- Support **CRUD operations** for students, instructors, lessons, and payments.  

### 🗄️ Database  
- Store student, instructor, lesson, and payment data using **MSSQL**.  
- Use **Entity Framework Core (EF Core)** for database interaction.  

## 🚀 Features  
✔ **User Authentication & Authorization** – Secure login system with **JWT authentication**.  
✔ **Student Management** – Register, update, and delete student profiles.  
✔ **Instructor Management** – Assign instructors to lessons and manage their availability.  
✔ **Lesson Scheduling** – Book, edit, and cancel driving lessons.  
✔ **Payment System** – Track payments for lessons and issue invoices.  
✔ **Role-Based Access Control (RBAC)** – Different privileges for **students, instructors, and administrators**.  
✔ **RESTful API** – API endpoints for managing all entities.  

## 🚗 Student (Client) Role  
### 📅 Lesson Booking  
- **Schedule, reschedule, or cancel** driving lessons.  
- View **upcoming and past lesson history**.  

### 📝 Progress Tracking  
- View **instructor feedback** and progress reports.  
- Track **completed and pending lesson milestones**.  

### 🔄 Profile Management  
- Update **personal details and preferences**.  
- Manage **account security and settings**.  
  
## 👨‍🏫 Instructor Role  
### 📋 Lesson Management  
- View **assigned lessons and schedules**.  
- Update **lesson status** (e.g., completed, canceled).  

### 🔄 Availability Control  
- Set **personal availability** for bookings.  
- Block **specific time slots** when unavailable.  

### 📊 Student Progress Monitoring  
- Provide **lesson feedback** and progress reports.  
- Assess **students** and mark skill levels.  
- **Receive and verify documents** from students.  
- **Conduct and grade internal driving exams**.  

## 🛠️ Administrator Role  
### 👤 User and Instructor Management  
- Add, update, or remove **instructors**.  
- Reset **passwords** and manage user credentials.  
- Enroll **students in courses** and manage their registrations.  

### 📅 Lesson & Instructor Management  
- Manage **theoretical lectures** and assign **instructors** to them.  
- Oversee **lesson schedules** without direct booking control over **practical lessons**.  

### 💰 Finance & Payments  
- **Define and update** lesson pricing.  
- Track **student payments** and confirm transactions.  
- **Manually process payments** when necessary.  

### 🛠 System Configuration  
- Update **service offerings** (e.g., adding new lesson types).

## 💼 My Role in the Project  
I was responsible for **everything** in this project. I designed, developed, and implemented the entire backend using **.NET 8**. My contributions include:  
- Designing and implementing the **Entity Framework Core database schema**.  
- Developing the **Web API in .NET 8**.  
- Implementing **authentication and authorization (JWT)**.  
- Creating and optimizing **API endpoints** for students, instructors, lessons, and payments.  
- Handling all aspects of the **backend infrastructure** and ensuring smooth communication with the database.  

## 🤝 Team Collaboration  
This project was a **solo development** effort. I independently handled all aspects of the backend.  

## 🛠️ Tech Stack  
- **Backend:** .NET 8, Web API, Entity Framework Core  
- **Database:** MSSQL  
- **Authentication:** JWT Tokens  
- **Other Tools:** Git, Swagger for API testing, Visual Studio  

