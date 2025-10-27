class Apartment {
  final int? id;
  final String? imageUrl;
  final String? link;
  final String? address;
  final int? price;
  final int? quality;
  final int? location;
  final int? aesthetics;
  final int? furniture;
  final String? phone;
  final String? observation;
  final String? chatLink;
  final num? mark;

  Apartment({
    this.id,
    this.imageUrl,
    this.link,
    this.address,
    this.price,
    this.quality,
    this.location,
    this.aesthetics,
    this.furniture,
    this.phone,
    this.observation,
    this.chatLink,
    this.mark,
  });

  factory Apartment.fromJson(Map<String, dynamic> json) {
    return Apartment(
      id: (json['id'] as num?)?.toInt(),
      imageUrl: json['imageUrl'],
      link: json['link'],
      address: json['address'],
      price: (json['price'] as num?)?.toInt(),
      quality: (json['quality'] as num?)?.toInt(),
      location: (json['location'] as num?)?.toInt(),
      aesthetics: (json['aesthetics'] as num?)?.toInt(),
      furniture: (json['furniture'] as num?)?.toInt(),
      phone: json['phone'],
      observation: json['observation'],
      chatLink: json['chatLink'],
      mark: json['mark'],
    );
  }
}
