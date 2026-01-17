# Image Upload Service – ASP.NET Core

A backend service built with **ASP.NET Core (.NET 6+)** that allows secure image uploading, validation, processing, and previewing.  
The project demonstrates best practices for file handling, image processing, and API design.

---

## 🚀 Features

### ✅ Image Upload
- Upload images using `IFormFile`
- Supported formats:
  - PNG
  - JPEG / JPG
  - GIF
- File size limit enforced

---

### ✅ Image Validation
- MIME type validation
- File size validation
- Prevents invalid or unsupported uploads

---

### ✅ Image Processing
- Image resizing while preserving aspect ratio
- Image compression during upload
- Uses **ImageSharp** for processing
- Optional format normalization (JPEG)

---

### ✅ Metadata Extraction
For each uploaded image, the following metadata is extracted and stored:
- Width
- Height
- Image format
- File size
- Upload timestamp

---

### ✅ Preview URLs (Local & Production Ready)
- Images are stored under `wwwroot/uploads`
- Static file middleware enabled
- Images can be previewed via URL:


### ✅ Global Exception Handling
- Centralized **Global Exception Middleware**
- Handles unexpected errors consistently
- Returns clean and meaningful error responses
- Prevents application crashes

---

### ✅ Image Deletion
- Images can be deleted by their owner
- Deletes both:
- Physical file
- Database record

---

## 🧱 Technology Stack

- **ASP.NET Core (.NET 6+)**
- **Entity Framework Core**
- **ImageSharp**
- RESTful API architecture

---

### Resize Rules
- Maximum width applied (e.g. 1200px)
- Aspect ratio preserved
- No upscaling for small images

### Compression
- JPEG quality optimized (70–80)
- PNG compression applied when applicable
- EXIF metadata removed for privacy

---

## 🔐 Security & Best Practices

- MIME type validation
- File size limits
- Server-side image processing
- Centralized error handling
- Clean separation of concerns

---
