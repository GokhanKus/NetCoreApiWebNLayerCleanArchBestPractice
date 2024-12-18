# Net Core API N-Layer Clean Architecture Projesi

### Açıklama
Bu proje, **.NET Core** kullanılarak geliştirilmiş, **N-Layer Architecture** ve **Clean Architecture** prensiplerine dayalı bir API uygulamasıdır. Proje, yazılım geliştirme en iyi uygulamalarına odaklanır ve scalable, maintainable bir yapı sunar.

---

## Proje Özellikleri
- **Katmanlı Mimari (N-Layer)**: Ayrıştırılmış API, Domain, Application, Persistence, Infrastructure katmanları.
- **Clean Architecture**: Domain merkezli bir yaklaşım.
- **Generic Repository** ve **Unit of Work** desenleri.
- **Dependency Injection** (DI) kullanımı.
- **Result Pattern**: Servislerde tekdüze bir sonuç yapısı.
- **FluentValidation** ile validasyon desteği.
- **AutoMapper** kullanarak DTO ve Entity dönüşümleri.
- **Global Exception Handling**: Exception yönetimi için merkezi yapı.
- **In-Memory Caching** desteği.
- **RabbitMQ** ile **Service Bus** entegrasyonu.

---

## Klasör Yapısı

```plaintext
CleanArchitecture/
│
├── Src/
│   ├── API/
│   │   └── App_API/                     # API katmanı
│   │       ├── Controllers/            # Controller sınıfları
│   │       ├── ExceptionHandler/       # Global exception handling
│   │       ├── Extension/              # Genişletmeler (DI vb.)
│   │       ├── Filters/                # Filtreler (Validation, NotFound vb.)
│   │       └── Program.cs              # Uygulama başlatma
│   │
│   ├── Core/
│   │   ├── App.Application/            # Uygulama katmanı
│   │   │   ├── Contracts/              # Servis sözleşmeleri
│   │   │   ├── Extensions/             # Genişletmeler
│   │   │   ├── Features/               # Özellikler (CQRS vb.)
│   │   │   └── ServiceResult.cs        # Result Pattern implementasyonu
│   │   │
│   │   ├── App.Domain/                 # Domain katmanı
│   │       ├── Entities/               # Entity sınıfları
│   │       ├── Const/                  # Sabitler
│   │       ├── Exceptions/             # Özel exception sınıfları
│   │       └── Event/                  # Event sınıfları
│   │
│   └── Infrastructure/
│       ├── App.Bus/                    # Service Bus (RabbitMQ) implementasyonu
│       ├── App.Caching/                # Cache mekanizması (In-Memory)
│       ├── App.Persistence/            # Veri erişim katmanı
│           ├── Categories/                 # Kategori ile ilgili dosyalar
│           ├── Extensions/                 # Genişletmeler
│           ├── Interceptors/               # EF Core interceptor sınıfları
│           ├── Migrations/                 # Migration dosyaları
│           ├── Products/                   # Ürünle ilgili dosyalar
│           ├── AppDbContext.cs             # Entity Framework Core DbContext
│           ├── GenericRepository.cs        # Generic Repository implementasyonu
│           ├── PersistenceAssembly.cs      # Persistence Assembly yapılandırması
│           └── UnitOfWork.cs               # Unit of Work implementasyonu

---
```

## Branch'ler ve açıklamaları

- **ServiceBus(RabbitMQ):** RabbitMQ tabanlı mesaj işleme sistemi, Service Bus üzerinden entegre edildi.
- **AddFeature(Caching):** In-Memory Caching özelliği eklendi.
- **CleanArchitecture:** Clean Architecture'a geçildi, eski katmanlar kaldırıldı ve proje yapısı domain, persistence, application ve API katmanları olarak yeniden düzenlendi.
- **BestPractices:** `BaseEntity` oluşturuldu, null kontrolü eklendi ve eksik kaynakları ele almak için `NotFoundFilter` tanımlandı.
- **APIDevelopment:** `AuditDbContextInterceptor` ve `IAuditEntity` ile denetim mekanizması entegre edildi.
- **ExceptionHandler:** Hatalar `CriticalException`, `CriticalExceptionHandler` ve `GlobalExceptionHandler` sınıflarıyla ele alındı.
- **AutoMapper:** DTO ve Entity dönüşümleri için AutoMapper entegrasyonu.
- **Validation:** FluentValidation kullanarak request validasyonları.
- **NLayer:** Daha iyi bir mimari ve veritabanı işlemleri yönetimi için repository deseni, service result deseni ve unit of work deseni gibi birkaç NLayer özelliği tanıtıldı.
