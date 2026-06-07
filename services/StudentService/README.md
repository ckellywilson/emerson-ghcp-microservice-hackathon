# StudentService

StudentService is the Day-2 extraction target for the workshop.

Current scope of this shell:
- Student aggregate root
- Enrollment child entity under Student ownership
- Course referenced by `CourseId` plus read-model projection data
- ACL placeholder for monolith-to-service translation

Local build:

```bash
dotnet build services/StudentService/StudentService.slnx
```