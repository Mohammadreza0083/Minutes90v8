# راهنمای Deployment در Railway

## مشکلات حل شده

✅ تغییر نسخه .NET SDK از preview به stable (8.0.413)  
✅ بهبود Dockerfile برای سازگاری بهتر  
✅ اضافه کردن health check endpoint  
✅ بهبود error handling و logging  

## متغیرهای محیطی مورد نیاز در Railway

### 1. DATABASE_URL
Connection string برای PostgreSQL database:
```
postgresql://username:password@host:port/database
```

### 2. TokenKey
کلید رمزنگاری برای JWT tokens (حداقل 32 کاراکتر):
```
your-super-secret-key-with-at-least-32-characters-for-production
```

### 3. PORT (اختیاری)
پورت که Railway به صورت خودکار تنظیم می‌کند.

## مراحل Deployment

1. **کد را به GitHub push کنید:**
   ```bash
   git add .
   git commit -m "Fix Railway deployment issues"
   git push
   ```

2. **در Railway:**
   - پروژه جدید ایجاد کنید
   - از GitHub repository خود انتخاب کنید
   - متغیرهای محیطی بالا را تنظیم کنید
   - Database PostgreSQL اضافه کنید
   - Deploy کنید

## بررسی سلامت برنامه

پس از deployment، می‌توانید سلامت برنامه را بررسی کنید:
```
https://your-app.railway.app/health
```

## عیب‌یابی

اگر هنوز مشکل دارید:

1. **Logs را بررسی کنید** در Railway dashboard
2. **متغیرهای محیطی** را دوباره چک کنید
3. **Database connection** را تست کنید
4. **TokenKey** حداقل 32 کاراکتر باشد

## نکات مهم

- از نسخه stable .NET 8.0.413 استفاده می‌کنیم
- Dockerfile بهینه شده برای Railway
- Health check endpoint اضافه شده
- Error handling بهبود یافته 