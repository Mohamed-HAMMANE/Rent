import 'package:flutter/material.dart';
import 'package:rent/models/apartment.dart';
import 'package:rent/services/api_service.dart';

class EditApartmentScreen extends StatefulWidget {
  final Apartment apartment;

  const EditApartmentScreen({super.key, required this.apartment});

  @override
  _EditApartmentScreenState createState() => _EditApartmentScreenState();
}

class _EditApartmentScreenState extends State<EditApartmentScreen> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _priceController;
  late TextEditingController _qualityController;
  late TextEditingController _locationController;
  late TextEditingController _aestheticsController;
  late TextEditingController _furnitureController;
  late TextEditingController _phoneController;
  late TextEditingController _observationController;
  final ApiService _apiService = ApiService();

  @override
  void initState() {
    super.initState();
    _priceController = TextEditingController(text: widget.apartment.price?.toString() ?? '');
    _qualityController = TextEditingController(text: widget.apartment.quality?.toString() ?? '');
    _locationController = TextEditingController(text: widget.apartment.location?.toString() ?? '');
    _aestheticsController = TextEditingController(text: widget.apartment.aesthetics?.toString() ?? '');
    _furnitureController = TextEditingController(text: widget.apartment.furniture?.toString() ?? '');
    _phoneController = TextEditingController(text: widget.apartment.phone ?? '');
    _observationController = TextEditingController(text: widget.apartment.observation ?? '');
  }

  void _saveApartment() async {
    if (_formKey.currentState!.validate()) {
      final data = {
        'price': int.tryParse(_priceController.text) ?? widget.apartment.price,
        'quality': int.tryParse(_qualityController.text) ?? widget.apartment.quality,
        'location': int.tryParse(_locationController.text) ?? widget.apartment.location,
        'aesthetics': int.tryParse(_aestheticsController.text) ?? widget.apartment.aesthetics,
        'furniture': int.tryParse(_furnitureController.text) ?? widget.apartment.furniture,
        'phone': _phoneController.text,
        'observation': _observationController.text,
      };

      try {
        await _apiService.updateApartment(widget.apartment.id!, data);
        Navigator.pop(context);
      } catch (e) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Failed to update apartment: $e')),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Edit Apartment'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: ListView(
            children: [
              TextFormField(
                controller: _priceController,
                decoration: const InputDecoration(labelText: 'Price'),
                keyboardType: TextInputType.number,
              ),
              TextFormField(
                controller: _qualityController,
                decoration: const InputDecoration(labelText: 'Quality'),
                keyboardType: TextInputType.number,
              ),
              TextFormField(
                controller: _locationController,
                decoration: const InputDecoration(labelText: 'Location'),
                keyboardType: TextInputType.number,
              ),
              TextFormField(
                controller: _aestheticsController,
                decoration: const InputDecoration(labelText: 'Aesthetics'),
                keyboardType: TextInputType.number,
              ),
              TextFormField(
                controller: _furnitureController,
                decoration: const InputDecoration(labelText: 'Furniture'),
                keyboardType: TextInputType.number,
              ),
              TextFormField(
                controller: _phoneController,
                decoration: const InputDecoration(labelText: 'Phone'),
              ),
              TextFormField(
                controller: _observationController,
                decoration: const InputDecoration(labelText: 'Observation'),
              ),
              const SizedBox(height: 20),
              ElevatedButton(
                onPressed: _saveApartment,
                child: const Text('Save'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
