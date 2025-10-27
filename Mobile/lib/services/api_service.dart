import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:rent/models/apartment.dart';

class ApiService {
  static const String _baseUrl = 'http://mohamed.homeip.net:8055';

  Future<List<Apartment>> getApartments() async {
    final response = await http.get(Uri.parse('$_baseUrl/bachelorpad'));

    if (response.statusCode == 200) {
      final List<dynamic> data = json.decode(response.body);
      return data.map((json) => Apartment.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load apartments');
    }
  }

  Future<void> updateApartment(int id, Map<String, dynamic> data) async {
    final response = await http.put(
      Uri.parse('$_baseUrl/bachelorpad/$id'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(data),
    );

    if (response.statusCode != 204) {
      throw Exception('Failed to update apartment');
    }
  }
}
